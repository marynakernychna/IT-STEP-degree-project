using Core.DTO.Characteristic;
using System;
using System.Collections.Generic;

namespace Core.DTO.Ware
{
    public class WareInfoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorFullName { get; set; }
        public double Cost { get; set; }
        public string PhotoBase64 { get; set; }
        public int AvailableCount { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string CategoryTitle { get; set; }
        public List<CharacteristicWithoutWareIdDTO> Characteristics { get; set; }
    }
}
