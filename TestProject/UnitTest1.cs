using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        MainWindow wnd = new MainWindow();

        [TestMethod]
        public void InsertBtnTest()
        {
            // input
            string name = "����� � �������";
            string price = "100";
            string author = "��������� ��������� ������";
            string category = "�����";

            wnd.InsertBook();
        }
    }
}
