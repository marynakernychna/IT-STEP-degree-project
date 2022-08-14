using Core.DTO.Characteristic;
using System.Collections.Generic;

namespace Core.DTO.Ware
{
    public class WareInfoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string PhotoLink { get; set; }
        public int AvailableCount { get; set; }
        public string CategoryTitle { get; set; }
        public List<CharacteristicWithoutWareIdDTO> Characteristics { get; set; }
    }
}
