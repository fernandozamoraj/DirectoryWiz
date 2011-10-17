using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirWizConsole
{
    public interface ICommand
    {
        void Execute();
    }

    public class RemoverCommand : ICommand
    {
        public RemoverCommand(string[] args)
        {

        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
