using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class SettingsProvider_Should
    {
        [WpfFact]
        public void Set_The_UI_OutputControl_To_SettingsUi()
        {
            var settingsProvider = new SettingsProvider();
            var repoMock = new Mock<ISettingsRepository>();
            repoMock.Setup(repository => repository.GetAll())
                .Returns(new ISettings[] {new FooSettings(), new BarSettings()});
            settingsProvider.SettingsRepository = repoMock.Object;
            var mock = new Mock<IUiFacade>();
            settingsProvider.UiFacade = mock.Object;
            var propertyEditorFactoryRepoMock = new Mock<IPropertyEditorFactoryRepository>();
            propertyEditorFactoryRepoMock.Setup(repository => repository.Get(It.IsAny<Type>()))
                .Returns(new StringPropertyEditorFactory());
            settingsProvider.PropertyEditorFactoryRepository = propertyEditorFactoryRepoMock.Object;
            settingsProvider.Provide("".Split());
            mock.Verify(facade => facade.SetOutputControl(It.IsAny<Settings>()), Times.Once);
        }

        [WpfFact]
        public void Save_Settings_If_Passed_Save_Option()
        {
            var settingsProvider = new SettingsProvider();
            var repoMock = new Mock<ISettingsRepository>();

            repoMock.Setup(repository => repository.GetAll()).Returns(new ISettings[]{new FooSettings(), new BarSettings(), });
            repoMock.Setup(repository => repository.Save<FooSettings>());
            repoMock.Setup(repository => repository.Save<BarSettings>());
            settingsProvider.SettingsRepository = repoMock.Object;
            
            settingsProvider.Provide("-s".Split());
            repoMock.Verify(repository => repository.SaveAll(), Times.Once);
        }
    }
}