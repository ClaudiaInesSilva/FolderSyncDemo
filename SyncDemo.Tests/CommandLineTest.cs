using SyncDemo.src;

namespace SysnDemo.Tests
{

    [TestFixture]
    public class CommandLineTests
    {
        private string _tempSourcePath;
        private string _tempReplicaPath;
        private string _tempLogFilePath;

        [SetUp]
        public void Setup()
        {
            _tempSourcePath = Path.Combine(Path.GetTempPath(), "source");
            _tempReplicaPath = Path.Combine(Path.GetTempPath(), "replica");
            _tempLogFilePath = Path.Combine(Path.GetTempPath(), "log.txt");

            Directory.CreateDirectory(_tempSourcePath);
            Directory.CreateDirectory(_tempReplicaPath);
            File.Create(_tempLogFilePath).Dispose();
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_tempLogFilePath)) File.Delete(_tempLogFilePath);
            if (Directory.Exists(_tempReplicaPath)) Directory.Delete(_tempReplicaPath);
            if (Directory.Exists(_tempSourcePath)) Directory.Delete(_tempSourcePath);
        }


        [Test]
        [Description("Test that checks if CommandLine initializes correctly with valid arguments.")]
        public void ValidArguments_ShouldInitializeCorrectly()
        {
            // Arrange

            var args = new[] { _tempSourcePath, _tempReplicaPath, _tempLogFilePath, "30" };

            // Act
            var commandLine = new CommandLine(args);

            // Assert

            Assert.Multiple(() =>
            {
                Assert.That(_tempSourcePath, Is.EqualTo(commandLine.SourcePath));
                Assert.That(_tempReplicaPath, Is.EqualTo(commandLine.ReplicaPath));
                Assert.That(_tempLogFilePath, Is.EqualTo(commandLine.LogFilePath));
                Assert.That(commandLine.SyncInterval, Is.EqualTo(30));
            });
        }

        [Test]
        public void MoreThanFourArguments_ShouldThrowArgumentException()
        {
            //Arrange
            var args = new[] { _tempSourcePath, _tempReplicaPath, _tempLogFilePath, "30", "fifthArgument" };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo("4 arguments expected: <sourcePath> <replicaPath> <logFilePath> <syncInterval>"));
        }

        [Test]
        public void LessThanFourArguments_ShouldThrowArgumentException()
        {
            //Arrange
            var args = new[] { _tempSourcePath, _tempReplicaPath, _tempLogFilePath };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo("4 arguments expected: <sourcePath> <replicaPath> <logFilePath> <syncInterval>"));
        }

        [Test]
        public void InvalidSourcePath_ShouldThrowArgumentException()
        {
            //Arrange
            var invalidSourcePath = @"C:\invalidsource";
            var args = new[] { invalidSourcePath, _tempReplicaPath, _tempLogFilePath, "30" };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo($"The source path '{invalidSourcePath}' does not exist or is not accessible."));
        }

        [Test]
        public void InvalidReplicaPath_ShouldThrowArgumentException()
        {
            //Arrange
            var invalidReplicaPath = @"C:\invalidsource";
            var args = new[] { _tempSourcePath, invalidReplicaPath, _tempLogFilePath, "30" };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo($"The replica path '{invalidReplicaPath}' does not exist or is not accessible."));
        }

        [Test]
        public void InvalidLogFilePath_ShouldThrowArgumentException()
        {
            //Arrange
            var invalidLogFilePath = @"C:\invalidlogpath\log.txt";
            var args = new[] { _tempSourcePath, _tempReplicaPath, invalidLogFilePath, "30" };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo($"The directory for the log file '{invalidLogFilePath}' does not exist or is not accessible."));
        }

        [Test]
        public void InvalidSyncInterval_ShouldThrowArgumentException()
        {
            //Arrange
            var args = new[] { _tempSourcePath, _tempReplicaPath, _tempLogFilePath, "5" };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo($"The period of synchronization (<syncInterval>) must be between 10 seconds and 3600 seconds."));

        }

        [Test]
        public void NonNumbericalSyncInterval_ShouldThrowArgumentException()
        {
            //Arrange
            var args = new[] { _tempSourcePath, _tempReplicaPath, _tempLogFilePath, "interval" };

            //Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new CommandLine(args));
            Assert.That(ex.Message, Is.EqualTo("The period of synchronization (<syncInterval>) must be between 10 seconds and 3600 seconds."));
        }

    }
}

