using AspChatTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspChatTask.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<AppUser> Users{ get; set; }
        public AppUser CurrentUser { get; set; }
    }
}
