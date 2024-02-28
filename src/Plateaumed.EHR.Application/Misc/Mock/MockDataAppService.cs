using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Organizations;
using Abp.UI;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.UserModel;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;
using TransactionStatus = Plateaumed.EHR.PatientWallet.TransactionStatus;

namespace Plateaumed.EHR.Misc.Mock;

public class MockDataAppService : EHRAppServiceBase, IMockDataAppService
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<StaffMember, long> _staffMemberRepository;
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
    private readonly IRepository<User, long> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly UserManager _userManager;
    private readonly RoleManager _roleManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
    private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
    private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
    private readonly IRepository<FacilityStaff, long> _facilityStaffRepository;
    private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
    private readonly IRepository<ItemPricing, long> _itemPricingRepository;
    private readonly IRepository<PricingDiscountSetting, long> _pricingSettingRepository;
    private readonly IRepository<ItemPricingCategory, long> _itemPricingCategoryRepository;
    
    private readonly IRepository<Wallet,long> _walletRepository;
    private readonly IRepository<WalletHistory,long> _walletHistoryRepository;
    private readonly IRepository<Invoice,long> _invoiceRepository;
    private readonly IRepository<PaymentActivityLog,long> _paymentActivityLogRepository;
    private readonly IPayInvoicesCommandHandler _payInvoicesCommandHandler;
    
    private readonly ICreateWalletFundingRequestHandler _createWalletFundingRequestHandler;


    public MockDataAppService(
        IUnitOfWorkManager unitOfWorkManager,
        IRepository<Patient, long> patientRepository,
        IRepository<StaffMember, long> lookup_staffMemberRepository,
        IRepository<PatientAppointment, long> patientAppointmentRepository,
        IRepository<User, long> userRepository,
        IRepository<Role> roleRepository,
        UserManager userManager,
        RoleManager roleManager,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        IRepository<OrganizationUnit, long> organizationUnitRepository,
        IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
        IRepository<FacilityStaff, long> facilityTypeRepository,
        IRepository<PatientCodeMapping, long> patientCodeMappingRepository, 
        IRepository<ItemPricing, long> itemPricingRepository,
        IRepository<PricingDiscountSetting, long> pricingSettingRepository,
        IRepository<ItemPricingCategory, long> itemPricingCategoryRepository, 
        IRepository<Wallet, long> walletRepository,
        IRepository<WalletHistory, long> walletHistoryRepository, 
        IRepository<Invoice, long> invoiceRepository, 
        IRepository<PaymentActivityLog, long> paymentActivityLogRepository, 
        ICreateWalletFundingRequestHandler createWalletFundingRequestHandler, IPayInvoicesCommandHandler payInvoicesCommandHandler)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _patientRepository = patientRepository;
        _staffMemberRepository = lookup_staffMemberRepository;
        _patientAppointmentRepository = patientAppointmentRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _passwordHasher = passwordHasher;
        _passwordValidators = passwordValidators;
        _organizationUnitRepository = organizationUnitRepository;
        _userOrganizationUnitRepository = userOrganizationUnitRepository;
        _facilityStaffRepository = facilityTypeRepository;
        _patientCodeMappingRepository = patientCodeMappingRepository;
        _itemPricingRepository = itemPricingRepository;
        _pricingSettingRepository = pricingSettingRepository;
        _itemPricingCategoryRepository = itemPricingCategoryRepository;
        _walletRepository = walletRepository;
        _walletHistoryRepository = walletHistoryRepository;
        _invoiceRepository = invoiceRepository;
        _paymentActivityLogRepository = paymentActivityLogRepository;
        _createWalletFundingRequestHandler = createWalletFundingRequestHandler;
        _payInvoicesCommandHandler = payInvoicesCommandHandler;
    }

    public async Task<dynamic> GetAllUserRoles()
    {
        return await _roleRepository.GetAll()
            .Where(v => v.TenantId == AbpSession.TenantId)
            .Select(v => new
            {
                v.Id,
                v.TenantId,
                v.Name,
                v.DisplayName
            })
            .ToListAsync();
    }

    public async Task<dynamic> CreateMockStaffMembers(int count, string[] roleNames)
    {
        var faker = new Faker();
        var responseList = new List<dynamic>();

        using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        {
            for (int i = 0; i < count; i++)
            {
                var dob = faker.Date.Past(50, DateTime.UtcNow.AddYears(-18));
                var phone = faker.Phone.PhoneNumber("+###########");
                var email = faker.Internet.Email().ToLower();

                var input = new CreateOrEditStaffMemberDto
                {
                    StaffMember = new StaffMemberDto
                    {
                        // TenantId = tenantId,
                        Gender = faker.PickRandom<GenderType>(),
                        UserName = email,
                        Name = faker.Name.FirstName(),
                        Surname = faker.Name.LastName(),
                        Password = "Plateaumed.123!",
                        EmailAddress = email,
                        PhoneNumber = phone,
                        DateOfBirth = dob,
                        ShouldChangePasswordOnNextLogin = false,
                        // Roles = new List<UserRole>(),
                        // OrganizationUnits = new List<UserOrganizationUnit>(),
                        // NormalizedEmailAddress = email,
                        // NormalizedUserName = email
                        StaffCode = faker.Random.String2(8)
                    },
                    AssignedRoleNames = roleNames,
                    SendActivationEmail = false,
                    SetRandomPassword = false
                };

                var user = ObjectMapper.Map<User>(input.StaffMember);
                user.TenantId = AbpSession.TenantId;

                var staffmember = ObjectMapper.Map<StaffMember>(input.StaffMember);
                user.StaffMemberFk = staffmember;

                //Set password
                if (input.SetRandomPassword)
                {
                    var randomPassword = await _userManager.CreateRandomPassword();
                    user.Password = _passwordHasher.HashPassword(user, randomPassword);
                    input.StaffMember.Password = randomPassword;
                }
                else if (!input.StaffMember.Password.IsNullOrEmpty())
                {
                    await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
                    foreach (var validator in _passwordValidators)
                    {
                        CheckErrors(await validator.ValidateAsync(UserManager, user, input.StaffMember.Password));
                    }

                    user.Password = _passwordHasher.HashPassword(user, input.StaffMember.Password);
                }

                user.ShouldChangePasswordOnNextLogin = input.StaffMember.ShouldChangePasswordOnNextLogin;
                user.IsActive = true;

                //Assign roles
                user.Roles = new Collection<UserRole>();
                foreach (var roleName in input.AssignedRoleNames)
                {
                    var role = await _roleManager.GetRoleByNameAsync(roleName);
                    user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
                }

                CheckErrors(await UserManager.CreateAsync(user));
                await _unitOfWorkManager.Current.SaveChangesAsync();

                //Organization Units
                await UserManager.SetOrganizationUnitsAsync(user, input.OrganizationUnits.ToArray());

                // initaie response object
                dynamic response = new ExpandoObject();
                response.UserId = user.Id;
                response.StaffMemberId = user.StaffMemberFk?.Id;
                response.Email = email;
                response.Password = "Plateaumed.123!";
                response.Name = input.StaffMember.Name;
                response.Surname = input.StaffMember.Surname;

                responseList.Add(response);
            }

            // commit transaction
            await uow.CompleteAsync();

            return responseList;
        }
    }

    public async Task<dynamic> GetOrganizationUnits()
    {
        return await _organizationUnitRepository.GetAll()
            .Where(v => v.TenantId == AbpSession.TenantId)
            .Select(v => new
            {
                v.Id,
                v.TenantId,
                v.ParentId,
                v.Code,
                v.DisplayName
            }).ToListAsync();
    }

    public async Task<dynamic> CreateAndMapOrganizationUnit(int userId, long? existingOrganizationUnitId = null)
    {
        if (await _userRepository.GetAll().AnyAsync(v => v.Id == userId && v.TenantId == AbpSession.TenantId) == false)
            throw new UserFriendlyException($"User not found for this tenant {AbpSession.TenantId}.");

        var faker = new Faker();

        // map to an existing organization unit
        if (existingOrganizationUnitId.HasValue)
        {
            var userOrgUnits = new UserOrganizationUnit
            {
                TenantId = AbpSession.TenantId,
                UserId = userId,
                OrganizationUnitId = existingOrganizationUnitId.Value,
                CreationTime = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userOrganizationUnitRepository.InsertAsync(userOrgUnits);
            await CurrentUnitOfWork.SaveChangesAsync();

            return userOrgUnits;
        }
        else
        {
            var orgUnit = new OrganizationUnit
            {
                TenantId = AbpSession.TenantId,
                Code = $"{faker.Random.UInt().ToString()}_{faker.Random.String2(8)}",
                DisplayName = faker.Company.CompanyName(),
                CreationTime = DateTime.UtcNow,
                IsDeleted = false
            };

            await _organizationUnitRepository.InsertAsync(orgUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            var userOrgUnits = new UserOrganizationUnit
            {
                TenantId = AbpSession.TenantId,
                UserId = userId,
                OrganizationUnitId = orgUnit.Id,
                CreationTime = DateTime.UtcNow,
                IsDeleted = false
            };

            await _userOrganizationUnitRepository.InsertAsync(userOrgUnits);
            await CurrentUnitOfWork.SaveChangesAsync();

            return userOrgUnits;
        }
    }

    public async Task CreateMockPatientAndAppointmentsForToday(int count,
        long? staffMemberOrAttendingPhysicianId = null, long? orgUnitOrAttendingClinicId = null)
    {
        var faker = new Faker();
        using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
        {
            for (int i = 0; i < count; i++)
            {
                var dob = faker.Date.Past(50, DateTime.UtcNow.AddYears(-18));
                var phone = faker.Phone.PhoneNumber("+###########");
                var email = faker.Internet.Email().ToLower();

                var patientDto = new CreateOrEditPatientDto
                {
                    // UserId = savedUser.Id,
                    PatientCode = $"{faker.Random.UInt().ToString()}",
                    Ethnicity = faker.Random.Word(),
                    Religion = faker.PickRandom<Religion>(),
                    MaritalStatus = faker.PickRandom<MaritalStatus>(),
                    BloodGroup = faker.PickRandom<BloodGroup>(),
                    BloodGenotype = faker.PickRandom<BloodGenotype>(),
                    NuclearFamilySize = faker.Random.Int(1, 10),
                    NumberOfSiblings = faker.Random.Int(0, 10),
                    PositionInFamily = faker.PickRandom("Father", "Mother", "Son", "Daughter"),
                    NumberOfChildren = faker.Random.Int(0, 10),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    EmailAddress = email,
                    /* Roles = savedUser.Roles,
                    OrganizationUnits = savedUser.OrganizationUnits, */
                    PhoneNumber = phone,
                    DateOfBirth = dob,
                    GenderType = faker.PickRandom<GenderType>()
                };

                var patient = ObjectMapper.Map<Patient>(patientDto);
                patient.UuId = Guid.NewGuid();

                // TODO: Maybe Implement facility checks from the StaffMember User currently logged in
                var facilityId = 1;

                if (facilityId <= 0) {

                    throw new UserFriendlyException("Current user facility is not set");
                }
                await CheckPatientCodeExistsInFacility(patientDto.PatientCode, facilityId);
                await _patientRepository.InsertAsync(patient);
                patient.PatientCodeMappings.Add(new PatientCodeMapping
                {
                    PatientCode = patientDto.PatientCode,
                    FacilityId = facilityId
                });
                // Initiate Patient
                var appointmentInput = new CreateOrEditPatientAppointmentDto
                {
                    Title = faker.Lorem.Sentence(),
                    Duration = 2,
                    StartTime = DateTime.UtcNow,
                    IsRepeat = false,
                    Status = faker.PickRandom(AppointmentStatusType.Awaiting_Doctor,
                        AppointmentStatusType.Awaiting_Clinician, AppointmentStatusType.Seen_Doctor, AppointmentStatusType.Awaiting_Vitals),
                    Type = faker.PickRandom<AppointmentType>()
                };
                var patientAppointment = ObjectMapper.Map<PatientAppointment>(appointmentInput);
                patientAppointment.TenantId = AbpSession.TenantId.HasValue == false ? 0 : AbpSession.TenantId.Value;
                patientAppointment.PatientFk = patient;
                patientAppointment.AttendingPhysicianId = staffMemberOrAttendingPhysicianId;
                patientAppointment.AttendingClinicId = orgUnitOrAttendingClinicId;

                await _patientAppointmentRepository.InsertAsync(patientAppointment);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }

            // commit transaction
            await uow.CompleteAsync();
        }
    }


    public async Task CreateMockCurrentLoginUserFacilityDetails()
    {
        var currentTenantId = AbpSession.TenantId;
        var currentUserId = AbpSession.UserId;
        

        var facilityGroup = new FacilityGroup
        {
            Name = "Crest Facility Group",
            TenantId = currentTenantId.GetValueOrDefault()
        };
        var patientTemplateCode = new PatientCodeTemplate()
        {
            Prefix = "P",
            Length = 10,
            Suffix = "A"
        };
        var pricingRule = new PricingDiscountSetting()
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            GlobalDiscount = 10
        };
        var facility = new Facility()
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Crest Facility",
            TypeId = 1,
            GroupFk = facilityGroup,
            PatientCodeTemplate = patientTemplateCode,

        };
        var jobTitle = new JobTitle()
        {
            Name = "Crest Job Title",
            TenantId = currentTenantId.GetValueOrDefault()

        };


        var staffMember = await _staffMemberRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == currentUserId);
        if (staffMember == null)
        {
            staffMember = new StaffMember
            {
                Jobs = new List<Job>
                {
                    new()
                    {
                        JobLevel = new JobLevel()
                        {
                            Name = "Crest Job Level",
                            TenantId = currentTenantId.GetValueOrDefault(),
                            JobTitleFk = jobTitle
                        },
                    }
                },
               
                UserId = currentUserId.GetValueOrDefault(),
            };
            var facilityStaff = new FacilityStaff()
            {
                FacilityFk = facility,
                StaffMemberFk = staffMember,
                IsDefault = true
            };
            await _facilityStaffRepository.InsertAsync(facilityStaff);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
        else
        {
            
            var facilityStaff = new FacilityStaff()
            {
                FacilityFk = facility,
                StaffMemberFk = staffMember,
                IsDefault = true
            };

            await _facilityStaffRepository.InsertAsync(facilityStaff);
            await _unitOfWorkManager.Current.SaveChangesAsync();

        }



    }

    public async Task GenerateClinicList(int numberOfClinics)
    {
        var currentUserId = AbpSession.UserId;
        var currentTenantId = AbpSession.TenantId;
        // add clinic to current facility;
        var existStaffMember = await _staffMemberRepository.GetAll().Include(x => x.AssignedFacilities)
            .FirstOrDefaultAsync(x => x.UserId == currentUserId);
        var existFacility = existStaffMember.AssignedFacilities.FirstOrDefault(x => x.IsDefault)?.FacilityId ?? 1;
        var fake = new Faker();

        for (int i = 0; i < numberOfClinics; i++)
        {
            var clinic = new OrganizationUnitExtended()
            {
                Code = fake.Random.String2(8),
                TenantId = currentTenantId.GetValueOrDefault(),
                DisplayName = fake.Company.CompanyName(),
                FacilityId = existFacility,
                Type = OrganizationUnitType.Clinic,
                IsActive = true,
            };
            await _organizationUnitRepository.InsertAsync(clinic);
        }

        await _unitOfWorkManager.Current.SaveChangesAsync();

    }
    public async Task GenerateInvoiceItems(int numberOfItems)
    {
        _itemPricingRepository.RemoveRange(_itemPricingRepository.GetAll().ToList());
        _pricingSettingRepository.RemoveRange(_pricingSettingRepository.GetAll().Where(x => x.TenantId == AbpSession.TenantId).ToList());
        _itemPricingCategoryRepository.RemoveRange(_itemPricingCategoryRepository.GetAll().ToList());

        var faker = new Faker();
        var currentTenantId = AbpSession.TenantId;
        var priceSettings = new PricingDiscountSetting
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            GlobalDiscount = 10
        };
        await _pricingSettingRepository.InsertAsync(priceSettings);
        await CurrentUnitOfWork.SaveChangesAsync();
        var category1 = new ItemPricingCategory
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Consultation",
            DiscountPercentage = 2
        };

        var category2 = new ItemPricingCategory
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Procedures",
            DiscountPercentage = 0
        };

        var category3 = new ItemPricingCategory
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Others",
            DiscountPercentage = 3
        };

        await _itemPricingCategoryRepository.InsertAsync(category1);
        await _itemPricingCategoryRepository.InsertAsync(category2);
        await _itemPricingCategoryRepository.InsertAsync(category3);
        await CurrentUnitOfWork.SaveChangesAsync();


        for (int i = 0; i < numberOfItems; i++)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = currentTenantId.GetValueOrDefault(),
                Name = faker.Commerce.ProductName(),
                Amount = new Money(faker.Finance.Amount()),
                ItemPricingCategoryId = i % 3 + 1 == 1 ? category1.Id : i % 3 + 1 == 2 ? category2.Id : category3.Id,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);

        }
        await CurrentUnitOfWork.SaveChangesAsync();



    }
    public async Task GenerateDummyInvoicePricing()
    {
        
        var currentTenantId = AbpSession.TenantId;
        var priceSettings = new PricingDiscountSetting
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            GlobalDiscount = 10
        };
        await _pricingSettingRepository.InsertAsync(priceSettings);
        await CurrentUnitOfWork.SaveChangesAsync();
        var category1 = new ItemPricingCategory
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Consultation",
            DiscountPercentage = 0
        };
        
        var category2 = new ItemPricingCategory
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Registration",
            DiscountPercentage = 0
        };
        
        
        await _itemPricingCategoryRepository.InsertAsync(category1);
        await _itemPricingCategoryRepository.InsertAsync(category2);
        await CurrentUnitOfWork.SaveChangesAsync();
        
       
        var itemPricing = new ItemPricing
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Patient Registration",
            Amount = new Money(2000),
            ItemPricingCategoryId = category2.Id,
        };
        
        var itemPricing2 = new ItemPricing
        {
            TenantId = currentTenantId.GetValueOrDefault(),
            Name = "Consultation Fee",
            Amount = new Money(1500),
            ItemPricingCategoryId = category1.Id,
        };
        await _itemPricingRepository.InsertAsync(itemPricing);
        await _itemPricingRepository.InsertAsync(itemPricing2);
        await CurrentUnitOfWork.SaveChangesAsync();
        
        

    }

    public async Task GeneratePaymentRecords(int numberOfRecords)
    {
        var faker = new Faker();
        var currentTenantId = AbpSession.TenantId;
        var facilityId = GetCurrentUserFacilityId();
    
        for (int i = 0; i < numberOfRecords; i++)
        {
           using var uow = _unitOfWorkManager.Begin();
           var patient = new Patient
           {
               FirstName = faker.Name.FirstName(),
               LastName = faker.Name.LastName(),
               GenderType = faker.PickRandom<GenderType>(),
               DateOfBirth = faker.Date.Past(10),
               EmailAddress = faker.Internet.Email(),
               PhoneNumber = faker.Phone.PhoneNumber(),
               Title = faker.PickRandom<TitleType>()
           };
           patient.PatientCodeMappings.Add(new PatientCodeMapping
           {
               PatientCode = $"{faker.Random.UInt().ToString()}",
               FacilityId = facilityId,
           });
           var wallet = new Wallet()
           {
               TenantId = currentTenantId.GetValueOrDefault(),
               Balance = new Money(0),
               Patient = patient,
               
           };
           await _walletRepository.InsertAsync(wallet);

           var wallethistory = new WalletHistory()
           {
               Amount = new Money(0),
               TenantId = currentTenantId.GetValueOrDefault(),
               FacilityId = facilityId,
               Patient = patient,
               Wallet = wallet,
               Source = TransactionSource.Indirect,
               TransactionType = TransactionType.Credit,
               Status = TransactionStatus.Approved,
               CurrentBalance = wallet.Balance,
           };
           await _walletHistoryRepository.InsertAsync(wallethistory);
           await _patientRepository.InsertAsync(patient);
           await _unitOfWorkManager.Current.SaveChangesAsync();
           var balance = wallet.Balance;
           var paymentLog = new PaymentActivityLog()
           {
               TenantId = currentTenantId.GetValueOrDefault(),
               FacilityId = facilityId,
               ActualAmount = wallet.Balance,
               Patient = patient,
               TransactionAction = TransactionAction.FundWallet,
               TransactionType = TransactionType.Credit,
               ToUpAmount = new Money(balance.Amount)
           };
           await _paymentActivityLogRepository.InsertAsync(paymentLog);
           var appointment = new PatientAppointment
           {
               TenantId = currentTenantId.GetValueOrDefault(),
               PatientFk = patient,
               Title = faker.PickRandom<AppointmentType>().ToString(),
               StartTime = faker.Date.BetweenOffset(DateTimeOffset.UtcNow.AddDays(-2), DateTimeOffset.UtcNow.AddDays(2)).DateTime,
               Duration = 2,
               IsRepeat = false,
               Status = faker.PickRandom<AppointmentStatusType>(),
               Notes = faker.Lorem.Sentence(),
               Type = faker.PickRandom<AppointmentType>(),
           };
           await _patientAppointmentRepository.InsertAsync(appointment);
           await _unitOfWorkManager.Current.SaveChangesAsync();
           var invoiceItems = new List<InvoiceItem>();
           for (int j = 0; j < faker.Random.Number(1, 5) ; j++)
           {
               var amount = new Money(faker.Finance.Amount());
               var invoiceItem = new InvoiceItem
               {
                   TenantId = currentTenantId.GetValueOrDefault(),
                   Name = faker.Commerce.ProductName(),
                   Quantity = faker.Random.Number(1, 5),
                   OutstandingAmount = amount,
                   SubTotal = amount,
                   FacilityId = facilityId,
                   Status = InvoiceItemStatus.Unpaid,
               };
               invoiceItem.SubTotal *= invoiceItem.Quantity;
               invoiceItem.OutstandingAmount = new Money(invoiceItem.SubTotal.Amount);
               invoiceItems.Add(invoiceItem);
           }

           var proforma = i % 4 != 0;
           var invoice = new Invoice
           {
               TenantId = currentTenantId.GetValueOrDefault(),
               PatientFk = patient,
               FacilityId = facilityId,
               InvoiceId = proforma ? string.Empty : $"{faker.Random.UInt().ToString()}",
               InvoiceSource = faker.PickRandom<InvoiceSource>(),
               InvoiceType = proforma ? InvoiceType.Proforma: InvoiceType.InvoiceCreation,
               PatientAppointmentFk = appointment,
               PaymentType = PaymentTypes.Wallet,
               PaymentStatus = PaymentStatus.Unpaid,
               TotalAmount = invoiceItems.Sum(x=>x.SubTotal),
               OutstandingAmount = invoiceItems.Sum(x => x.OutstandingAmount),
               InvoiceItems = invoiceItems,

           };
           
           await _invoiceRepository.InsertAsync(invoice);
           await _unitOfWorkManager.Current.SaveChangesAsync();

           foreach (var item in invoiceItems)
           {
               var invoiceActivityLog = new PaymentActivityLog()
               {
                   TenantId = currentTenantId.GetValueOrDefault(),
                   ActualAmount = new Money(item.SubTotal.Amount),
                   Patient = patient,
                   TransactionAction = TransactionAction.CreateInvoice,
                   FacilityId = facilityId,
                   Invoice = invoice,
                   InvoiceItemId = item.Id,
                   OutStandingAmount = new Money(item.OutstandingAmount.Amount),
                   InvoiceNo = invoice.InvoiceId,

               };
               await _paymentActivityLogRepository.InsertAsync(invoiceActivityLog);
           }
           await _unitOfWorkManager.Current.SaveChangesAsync();
           await uow.CompleteAsync();
        }
    }
    
    public async Task MockFundAndFinalizeRequest(long patentId)
    {
     var invoices = await _invoiceRepository.GetAll().Include(x =>x.InvoiceItems)
         .Where(x => x.PatientId == patentId).ToListAsync();
     var facilityId = GetCurrentUserFacilityId();
     foreach (var invoice in invoices)
     {
         var request = new WalletFundingRequestDto
         {
             PatientId = patentId,
             InvoiceItems = invoice.InvoiceItems.Select(x => new WalletFundingItem
             {
                 InvoiceId = invoice.Id,
                 Id = x.Id,
                 SubTotal = x.SubTotal.ToMoneyDto()
             }).ToList(),
             TotalAmount = invoice.TotalAmount.ToMoneyDto(),
             AmountToBeFunded = invoice.TotalAmount.ToMoneyDto(),
         };
         await _createWalletFundingRequestHandler.Handle(request, facilityId, AbpSession.TenantId.GetValueOrDefault());
     }
    }

    public async Task GenerateEditedInvoices(int number)
    {
        var currentTenantId = AbpSession.TenantId;
        var faker = new Faker();
        var facilityId = GetCurrentUserFacilityId();
        for(var i = 0 ; i < number; i++)
        {
            using var uow = _unitOfWorkManager.Begin();
            var patient = new Patient
            {
                FirstName = faker.Name.FirstName(),
                LastName = faker.Name.LastName(),
                GenderType = faker.PickRandom<GenderType>(),
                DateOfBirth = faker.Date.Past(10),
                EmailAddress = faker.Internet.Email(),
                PhoneNumber = faker.Phone.PhoneNumber(),
                Title = faker.PickRandom<TitleType>()
            };
            await _patientRepository.InsertAsync(patient);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            var invoiceItems = new List<InvoiceItem>();
            for (int j = 0; j < faker.Random.Number(1, 5); j++)
            {
                var amount = new Money(faker.Finance.Amount());
                var invoiceItem = new InvoiceItem
                {
                    TenantId = currentTenantId.GetValueOrDefault(),
                    Name = faker.Commerce.ProductName(),
                    Quantity = faker.Random.Number(1, 5),
                    OutstandingAmount = amount,
                    SubTotal = amount,
                    FacilityId = facilityId,
                    Status = InvoiceItemStatus.Unpaid,
                };
                invoiceItem.SubTotal *= invoiceItem.Quantity;
                invoiceItem.OutstandingAmount = new Money(invoiceItem.SubTotal.Amount);
                invoiceItems.Add(invoiceItem);
            }

            var proforma = i % 4 != 0;
            var invoice = new Invoice
            {
                TenantId = currentTenantId.GetValueOrDefault(),
                PatientFk = patient,
                FacilityId = facilityId,
                InvoiceId = proforma ? string.Empty : $"{faker.Random.UInt().ToString()}",
                InvoiceSource = faker.PickRandom<InvoiceSource>(),
                InvoiceType = proforma ? InvoiceType.Proforma : InvoiceType.InvoiceCreation,
                PaymentType = PaymentTypes.Wallet,
                PaymentStatus = PaymentStatus.Unpaid,
                TotalAmount = invoiceItems.Sum(x => x.SubTotal),
                OutstandingAmount = invoiceItems.Sum(x => x.OutstandingAmount),
                InvoiceItems = invoiceItems,
                IsEdited = true
            };

            await _invoiceRepository.InsertAsync(invoice);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            foreach (var item in invoiceItems)
            {
                var random = new Random().Next(1, 9);

                var invoiceActivityLog = new PaymentActivityLog()
                {
                    TenantId = currentTenantId.GetValueOrDefault(),
                    ActualAmount = new Money(item.SubTotal.Amount),
                    Patient = patient,
                    TransactionAction = TransactionAction.EditInvoice,
                    FacilityId = facilityId,
                    EditAmount = new Money(item.SubTotal.Amount - 3),
                    Invoice = invoice,
                    InvoiceItemId = item.Id,
                    OutStandingAmount = new Money(item.OutstandingAmount.Amount),
                    InvoiceNo = invoice.InvoiceId,
                    CreationTime = DateTime.Today.AddDays(-random)
                };
                await _paymentActivityLogRepository.InsertAsync(invoiceActivityLog);
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
            await uow.CompleteAsync();
        }
    }

    public async Task GeneratePricingListForEquitySpecialistHospital()
    {
        var facilityId = GetCurrentUserFacilityId();
        var tenantId = AbpSession.TenantId ?? throw new UserFriendlyException("TenantId is required");
        var consultationPricingCategory = new ItemPricingCategory
        {
            Name = "Consultation",
            TenantId = tenantId,
            PricingCategory = PricingCategory.Consultation,
            DiscountPercentage = 0.0m,
            
        };
        var wardPricingCategory = new ItemPricingCategory
        {
            Name = "Ward",
            TenantId = tenantId,
            PricingCategory = PricingCategory.WardAdmission,
            DiscountPercentage = 0.0m
        };
        var procedurePricingCategory = new ItemPricingCategory
        {
            Name = "Procedure",
            TenantId = tenantId,
            PricingCategory = PricingCategory.Procedure,
            DiscountPercentage = 0.0m
        };
        var labPricingCategory = new ItemPricingCategory
        {
            Name = "Laboratory",
            TenantId = tenantId,
            PricingCategory = PricingCategory.Laboratory,
            DiscountPercentage = 0.0m
        };
        using var uow = _unitOfWorkManager.Begin();
        
        await _itemPricingCategoryRepository.InsertAsync(consultationPricingCategory);
        await _itemPricingCategoryRepository.InsertAsync(wardPricingCategory);
        await _itemPricingCategoryRepository.InsertAsync(procedurePricingCategory);
        await _itemPricingCategoryRepository.InsertAsync(labPricingCategory);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        
        var registrationData = EquityHospitalMockData.GetRegistrationPricing();
        foreach (var pricingData in registrationData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = consultationPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        var consultationData = EquityHospitalMockData.GetConsultationPricing();
        foreach (var pricingData in consultationData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = consultationPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
            
        }
        
        var wardData = EquityHospitalMockData.GetWardPricing();
        foreach (var pricingData in wardData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = wardPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        var ambulanceData = EquityHospitalMockData.GetAmbulancePricing();
        foreach (var pricingData in ambulanceData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = wardPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        var theaterData = EquityHospitalMockData.GetTheatrePricingData();
        foreach (var pricingData in theaterData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = procedurePricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        var laboratoryData = EquityHospitalMockData.GetLabPricingData();
        foreach (var pricingData in laboratoryData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = labPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        var imagingData = EquityHospitalMockData.GetImagingPricingData();
        foreach (var pricingData in imagingData)
        {
            var itemPricing = new ItemPricing
            {
                TenantId = tenantId,
                FacilityId = facilityId,
                PricingType = PricingType.GeneralPricing,
                Name = pricingData.Name,
                SubCategory = pricingData.SubCategory,
                Amount = new Money(pricingData.Amount.Amount),
                ItemPricingCategoryId = labPricingCategory.Id,
                IsActive = true,
            };
            await _itemPricingRepository.InsertAsync(itemPricing);
        }
        
        await uow.CompleteAsync();
    }

    public async Task MockInvoiceAsPaid(long patientId)
    {
        var invoice = await _invoiceRepository.GetAll().Include(x => x.InvoiceItems)
            .FirstOrDefaultAsync(x => x.PatientId == patientId);
        if (invoice == null)
        {
            throw new UserFriendlyException("The user does not have any invoice");
        }
        var itemsToPay = new WalletFundingRequestDto
        {
            PatientId = patientId,
            TotalAmount = invoice.TotalAmount.ToMoneyDto(),
            AmountToBeFunded = invoice.InvoiceItems.Sum(x=>x.SubTotal).ToMoneyDto(),
            InvoiceItems = invoice.InvoiceItems.Select(x => new WalletFundingItem
            {
                InvoiceId = invoice.Id,
                Id = x.Id,
                SubTotal = x.SubTotal.ToMoneyDto()
            }).ToList(),
            
        };

        await _payInvoicesCommandHandler.Handle(itemsToPay);
    }

    private async Task CheckPatientCodeExistsInFacility(string patientCode, long facilityId)
    {
        var isPatientCodeExistInFacility = await _patientCodeMappingRepository.FirstOrDefaultAsync(
            x => x.FacilityId == facilityId && x.PatientCode.Equals(patientCode));
        if (isPatientCodeExistInFacility != null)
        {
            throw new UserFriendlyException("Patient code already exist in this facility");
        }
    }

}