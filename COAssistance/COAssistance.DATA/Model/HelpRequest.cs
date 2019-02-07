using COAssistance.COMMONS.Models;
using COAssistance.DATA.Model.HelpRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.DATA.Model
{
    public class HelpRequest
    {
        public int Id { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public HelpRequestCategory HelpRequestCategory { get; set; }

        [Required]
        public bool IsSolved { get; set; }

        [Required]
        public double Langitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string ClientId { get; set; }

        public Client Client { get; set; }

        public ICollection<HelpRequestResponse> HelpRequestResponses { get; set; }
    }
}