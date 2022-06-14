using Microsoft.VisualStudio.TestTools.UnitTesting;
using Peter_image_sorter;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DateExtractFromFilenameWorksWith_s21Heic()
        {
            // setup
            string fileName = "20211114_190938.heic";

            DateObject expected = new DateObject();
            expected.Day = "14";
            expected.Month = "11";
            expected.Year = "2021";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_WhatsApp()
        {
            // setup
            string fileName = "VID-20211112-WA0008.mp4";

            DateObject expected = new DateObject();
            expected.Day = "12";
            expected.Month = "11";
            expected.Year = "2021";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProHeic()
        {
            // setup
            string fileName = "IMG20220122112747.heic";

            DateObject expected = new DateObject();
            expected.Day = "22";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_EufyVideo()
        {
            // setup
            string fileName = "_2022_06_08_19_46_08.mp4";

            DateObject expected = new DateObject();
            expected.Day = "08";
            expected.Month = "06";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProVideo()
        {
            // setup
            string fileName = "VID_20220103_205223.mp4";

            DateObject expected = new DateObject();
            expected.Day = "03";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_S21Video()
        {
            // setup
            string fileName = "20220102_191014.mp4";

            DateObject expected = new DateObject();
            expected.Day = "02";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }

        [TestMethod]
        public void DateExtractFromFilenameWorksWith_OnePlus9ProVideo_2()
        {
            // setup
            string fileName = "VID20220126210857.mp4";

            DateObject expected = new DateObject();
            expected.Day = "26";
            expected.Month = "01";
            expected.Year = "2022";

            // action
            var actual = Peter_image_sorter.Program.GetDateFromFileName(fileName);

            // assert
            datesMatch(expected, actual);
        }
        private void datesMatch(DateObject expected, DateObject actual)
        {
            // assert that the day, month and year match what they should be
            Assert.AreEqual(expected.Day, actual.Day);
            Assert.AreEqual(expected.Month, actual.Month);
            Assert.AreEqual(expected.Year, actual.Year);
        }

    }
}
