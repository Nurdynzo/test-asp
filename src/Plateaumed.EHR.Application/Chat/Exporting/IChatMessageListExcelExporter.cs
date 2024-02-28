using System.Collections.Generic;
using Abp;
using Plateaumed.EHR.Chat.Dto;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
