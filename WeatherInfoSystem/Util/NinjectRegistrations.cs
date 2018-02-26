using Ninject.Modules;
using WeatherInfoSystem.ThirdParty;

namespace WeatherInfoSystem.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IOpenWeatherManager>().To<OpenWeatherManager>();
        }
    }
}
