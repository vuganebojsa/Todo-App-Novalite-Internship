using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs.Attachment
{
    public class CreateAttachmentResponseDTO
    {
        public string SasToken { get; set; }
        public AttachmentDTO Attachment { get; set; }

        public CreateAttachmentResponseDTO(string sasToken, AttachmentDTO attachment)
        {
            SasToken = sasToken;
            Attachment = attachment;
        }
    }
}
