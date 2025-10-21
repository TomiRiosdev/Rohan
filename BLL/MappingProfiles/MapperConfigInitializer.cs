using AutoMapper;
using System;

namespace BLL.MappingProfiles
{
    public static class MapperConfigInitializer
    {
        // Usamos Lazy para asegurar que la configuración solo ocurra UNA VEZ y sea thread-safe.
        private static readonly Lazy<IMapper> LazyMapper = new Lazy<IMapper>(() =>
        {
            // CORRECCIÓN: Usamos el constructor de MapperConfiguration que acepta un Type
            // O, más simple, pasamos el objeto del perfil directamente al método AddProfile

            var config = new MapperConfiguration(cfg =>
            {
                // Se registra el perfil que definiste, usando la instancia del perfil
                cfg.AddProfile(new GeneralProfile());
            });

            // Opcional pero recomendado: Verificar que el mapeo sea válido
            //config.AssertConfigurationIsValid();

            return config.CreateMapper();
        });

        public static IMapper Mapper => LazyMapper.Value;
    }
}
