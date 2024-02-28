using System.Collections.Generic;
using Plateaumed.EHR.Auditing.Dto;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
