using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_libraries.DTOs
{
    public class UpdateMessageDto
    {
        public UpdateMessageDto()
        {
            
        }
        public int messageId { get; set; }
        public string updateToUser { get; set; }
        public string msg {  get; set; }
    }
}
