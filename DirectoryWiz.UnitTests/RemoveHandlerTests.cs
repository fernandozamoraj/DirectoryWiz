using DirectoryWiz.Framework.Api;
using DirectoryWiz.Framework.CommandLineHelpers;
using DirectoryWiz.Framework.CommandLineHelpers.Handlers;
using Moq;
using NUnit.Framework;

namespace DirectoryWiz.UnitTests
{
    public class RemoveHandlerContext
    {
        protected RemoveHandler RemoveHandler;
        protected Moq.Mock<IFileRemover> _fileRemoverMock;
        protected Moq.Mock<IDivLogger> _divLoggerMock;

        [TestFixtureSetUp]
        public virtual void Context()
        {
            _divLoggerMock = new Mock<IDivLogger>();
            _fileRemoverMock = new Mock<IFileRemover>();

        }
    }

    [TestFixture]
    public class When_remove_by_folder_extensions_is_called : RemoveHandlerContext
    {
        public override void Context()
        {
            base.Context();

            _fileRemoverMock.Setup(
                fileRemover => fileRemover.RemoveFileByExtensions(It.IsAny<string>(), It.IsAny<string[]>()))
                .AtMostOnce();

            RemoveHandler = new RemoveHandler(_fileRemoverMock.Object);
            RemoveHandler.HandleRequest(new[]{"--remove", "-e", "rootpath", ".ext1 .ext2"}, _divLoggerMock.Object);

        }

        [Test]
        public void Should_execute_the_remove_file_by_extensions_method()
        {
            _fileRemoverMock.Verify(x => x.RemoveFileByExtensions(It.IsAny<string>(), It.IsAny<string[]>()));
 
        }
    }

    [TestFixture]
    public class When_remove_by_folder_filename_is_called : RemoveHandlerContext
    {
        public override void Context()
        {
            base.Context();

            _fileRemoverMock.Setup(
                fileRemover => fileRemover.RemoveFileByName(It.IsAny<string>(), It.IsAny<string[]>()))
                .AtMostOnce();

            RemoveHandler = new RemoveHandler(_fileRemoverMock.Object);
            RemoveHandler.HandleRequest(new[] { "--remove", "-n", "rootpath", "file2.txt file2.txt" }, _divLoggerMock.Object);
        }

        [Test]
        public void Should_execute_the_remove_file_by_extensions_method()
        {
            _fileRemoverMock.Verify(x => x.RemoveFileByName(It.IsAny<string>(), It.IsAny<string[]>()));
        }
    }

    [TestFixture]
    public class When_remove_by_folder_name : RemoveHandlerContext
    {
        public override void Context()
        {
            base.Context();

            _fileRemoverMock.Setup(
                fileRemover => fileRemover.RemoveFolderByName(It.IsAny<string>(), It.IsAny<string[]>()))
                .AtMostOnce();

            RemoveHandler = new RemoveHandler(_fileRemoverMock.Object);
            RemoveHandler.HandleRequest(new[] { "--remove", "-fn", "rootpath", ".svn bin debug" }, _divLoggerMock.Object);
        }

        [Test]
        public void Should_execute_the_remove_file_by_extensions_method()
        {
            _fileRemoverMock.Verify(x => x.RemoveFolderByName(It.IsAny<string>(), It.IsAny<string[]>()));
        }
    }

    [TestFixture]
    public class When_remove_by_regular_expression : RemoveHandlerContext
    {
        public override void Context()
        {
            base.Context();

            _fileRemoverMock.Setup(
                fileRemover => fileRemover.RemoveByRegularExpression(It.IsAny<string>(), It.IsAny<string>()))
                .AtMostOnce();

            RemoveHandler = new RemoveHandler(_fileRemoverMock.Object);
            RemoveHandler.HandleRequest(new[] { "--remove", "-rx", "rootpath", "[A-Z][a-z]" }, _divLoggerMock.Object);
        }

        [Test]
        public void Should_execute_the_remove_file_by_extensions_method()
        {
            _fileRemoverMock.Verify(x => x.RemoveByRegularExpression(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
