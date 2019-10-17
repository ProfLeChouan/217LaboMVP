using PluralsightWinFormsDemoApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.Presenter
{
    class MainPresenter
    {
        private readonly IMainForm myMainForm;
        public MainPresenter(IMainForm aMainForm)
        {
            myMainForm = aMainForm;
        }
    }
}
