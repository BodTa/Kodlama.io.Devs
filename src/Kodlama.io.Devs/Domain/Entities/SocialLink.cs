using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SocialLink:Entity
    {
        public string Url { get; set; }
        public int UserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public SocialLink()
        {

        }
        public SocialLink(string url, int userId,int id)
        {
            Id = id;
            Url = url;
            UserId = userId;
        }
    }
}
