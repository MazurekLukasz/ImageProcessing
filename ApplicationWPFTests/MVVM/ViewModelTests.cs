using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationWPF.Tests
{
    [TestClass()]
    public class ViewModelTests
    {
        [TestMethod()]
        public void LoadImageFromDiscTest()
        {
            ViewModel vm = new ViewModel();
            vm.LoadImageFromDisc(null);
        }

        [TestMethod()]
        public void ConvertImageTest()
        {
            ViewModel vm = new ViewModel();
            vm.ConvertImage();
        }

        [TestMethod()]
        public void SaveImageOnDiscTest()
        {
            ViewModel vm = new ViewModel();
            vm.SaveImageOnDisc(null);
        }
    }
}