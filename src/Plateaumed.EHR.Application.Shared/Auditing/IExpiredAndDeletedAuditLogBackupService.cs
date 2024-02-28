using System.Collections.Generic;
using Abp.Auditing;

namespace Plateaumed.EHR.Auditing
{
    public interface IExpiredAndDeletedAuditLogBackupService
    {
        bool CanBackup();
        
        void Backup(List<AuditLog> auditLogs);
    }
}