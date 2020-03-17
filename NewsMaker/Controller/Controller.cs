using System;

namespace WindowsFormsApp1.Controller
{
    public class Controller
    {
        public void GetCommand(string command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
        }

    }
}
