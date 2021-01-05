using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArch.Domain.Entities
{
    public class AddressInfo
    {
        public int Id { get; set; }

        [ForeignKey("City")]
        public int GovId { get; set; }

        [ForeignKey("Governate")]
        public int CityId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string BuildingNumber { get; set; }

        public string FlatNumber { get; set; }

        public virtual City City { get; set; }

        public virtual Governate Governate { get; set; }

        public virtual User User { get; set; }
    }
}
