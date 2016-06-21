using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermometerApp;
using System.Collections.Generic;
using System.Windows.Input;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void MainModelThermometerList()
        {
            
            MainModel mainModel = new MainModel();
            List<Thermometer> ThermometerList = new List<Thermometer>();

            for (int i = 1; i < 11; i++)
            {

                mainModel.SerialNr = (i + 1000000).ToString();
                mainModel.WarningTemperature = (2 * i + 5).ToString();
                mainModel.AlarmTemperature = (2 * i + 25).ToString();
                mainModel.ModelThermometerType = (MeasureType)Enum.ToObject(typeof(MeasureType), i % 3);
                if (mainModel.canCreateThermometer())
                {
                    ThermometerList.Add(mainModel.CreateThermometer());
                }
            }
            while (ThermometerList.Count > 0)
            {
                for (int i = 0; i < ThermometerList.Count; i++)
                {
                    Thermometer test = ThermometerList[i];

                    Thermometer actual = mainModel.Repository.Thermometers[i];
                    Assert.AreEqual(test.AlarmTemperature, actual.AlarmTemperature);
                    Assert.AreEqual(test.WarningTemperature, actual.WarningTemperature);
                    Assert.AreEqual(test.SerialNr, actual.SerialNr);
                }
                ThermometerList[0].Remove();
                ThermometerList.RemoveAt(0);
            }
            //Skaber nye thermometre med de sammen Serienumre
            for (int i = 1; i < 11; i++)
            {
                mainModel.SerialNr = (i + 1000000).ToString();
                mainModel.WarningTemperature = (2 * i + 5).ToString();
                mainModel.AlarmTemperature = (2 * i + 25).ToString();
                mainModel.ModelThermometerType = (MeasureType)Enum.ToObject(typeof(MeasureType), i % 3);
                if (mainModel.canCreateThermometer())
                {
                    ThermometerList.Add(mainModel.CreateThermometer());
                }
            }


        }

        [TestMethod]
        public void MainWindowViewModelCorrectValues()
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            vm.Model.AlarmTemperature = "70";
            vm.Model.WarningTemperature = "30";
            vm.Model.SerialNr = "1234567";
            vm.Model.ModelThermometerType = MeasureType.Normal;
            Assert.AreEqual(true, vm.Model.canCreateThermometer());
            vm.Model.CreateThermometer();
            Assert.AreEqual(null, vm.Model.Error);

        }

        [TestMethod]
        public void MainWindowViewModelInvalidSerial()
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            vm.Model.AlarmTemperature = "80";
            vm.Model.WarningTemperature = "30";
            vm.Model.SerialNr = "serial";
            vm.Model.ModelThermometerType = MeasureType.Normal;
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Serial number must be number.\n", vm.Model.Error);
            vm.Model.SerialNr = "123";
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Serial number must be a 7 digits number.\n", vm.Model.Error);
            vm.Model.SerialNr = "7654321";
            if (vm.Model.canCreateThermometer())
            {
                vm.Model.CreateThermometer();
            }
            vm.Model.SerialNr = "7654321";
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Serial Number already exsists.\n", vm.Model.Error);

        }

        [TestMethod]
        public void MainWindowViewModelInvalidValueTemperatures()
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            vm.Model.AlarmTemperature = "hej";
            vm.Model.WarningTemperature = "30";
            vm.Model.SerialNr = "2222222";
            vm.Model.ModelThermometerType = MeasureType.Normal;
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Alarm temperature must be a number.\n", vm.Model.Error);
            vm.Model.AlarmTemperature = "20";
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Warning temperature must be lower than Alarm temperature.\n", vm.Model.Error);
            vm.Model.WarningTemperature = "-10";
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Warning temperature must be higher than 0.\n", vm.Model.Error);
            vm.Model.WarningTemperature = "30";
            vm.Model.AlarmTemperature = "110";
            vm.Model.canCreateThermometer();
            Assert.AreEqual("Alarm temperature must be lower than 100.\n", vm.Model.Error);

        }

        [TestMethod]
        public void GridViewModel()
        {
            MainModel mainModel = new MainModel();
            List<Thermometer> ThermometerList = new List<Thermometer>();

            for (int i = 1; i < 11; i++)
            {

                mainModel.SerialNr = (i + 1000000).ToString();
                mainModel.WarningTemperature = (2 * i + 5).ToString();
                mainModel.AlarmTemperature = (2 * i + 25).ToString();
                mainModel.ModelThermometerType = (MeasureType)Enum.ToObject(typeof(MeasureType), i % 3);
                if (mainModel.canCreateThermometer())
                {
                    ThermometerList.Add(mainModel.CreateThermometer());
                }
            }
            GridViewModel gridVM = new GridViewModel();
            while (ThermometerList.Count > 0)
            {
                for (int i = 0; i < mainModel.Repository.Thermometers.Count; i++)
                {
                    Assert.AreEqual(ThermometerList[i], mainModel.Repository.Thermometers[i]);
                    Assert.AreEqual(ThermometerList[i], gridVM[i]);
                }
                ThermometerList[0].Remove();
                ThermometerList.RemoveAt(0);
            }
        }

        [TestMethod]
        public void MainWindowViewModelCreateCommand()
        {
            MainWindowViewModel vm = new MainWindowViewModel();
            ICommand CreateCommand = vm.CreateCommand;
            Assert.AreEqual(false, CreateCommand.CanExecute(vm));
            vm.Model.AlarmTemperature = "80";
            vm.Model.WarningTemperature = "4f";
            Assert.AreEqual(false,CreateCommand.CanExecute(vm));
            vm.Model.SerialNr = "2222222";
            Assert.AreEqual(false,CreateCommand.CanExecute(vm));
            vm.Model.WarningTemperature = "90";
            Assert.AreEqual(false,CreateCommand.CanExecute(vm));
             vm.Model.WarningTemperature = "10";
            Assert.AreEqual(true,CreateCommand.CanExecute(vm));
            if (vm.Model.canCreateThermometer())
            {
                vm.Model.CreateThermometer();
            }
            vm.Model.AlarmTemperature = "70";
            vm.Model.AlarmTemperature = "30";
            vm.Model.SerialNr = "2222222";
            Assert.AreEqual(false,CreateCommand.CanExecute(vm));
            vm.Model.SerialNr = "2222224";
            Assert.AreEqual(true,CreateCommand.CanExecute(vm));
            ICommand ClearCommand = vm.ClearCommand;
            ClearCommand.Execute(vm);
            Assert.AreEqual(false,CreateCommand.CanExecute(vm));

        }

     }

}
