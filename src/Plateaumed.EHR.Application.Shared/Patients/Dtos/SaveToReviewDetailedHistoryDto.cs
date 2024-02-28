using Plateaumed.EHR.Insurance;

using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class ReviewDetailedHistoryReturnDto : EntityDto<long>
    {
        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public FirstVisitNoteDto FirstVisitNote { get; set; }
        public bool? IsAutoSaved { get; set; }
    }

    public class SaveToReviewDetailedHistoryRequestDto
    {
        public long? Id { get; set; }
        [Required]
        public long EncounterId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public FirstVisitNoteDto FirstVisitNote { get; set; }
        public bool? IsAutoSaved { get; set; }
    }
}