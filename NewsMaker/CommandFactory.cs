using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Service;

namespace WindowsFormsApp1
{
    class CommandFactory
    {
        public void CallCommand(string command)
        {
            switch(command)
            {
                case "getNewArticle": 
                    new ArticleCreatorService();
                    break;
                default: 
                   throw new Exception("No such command");
            }
        }
    }
}
