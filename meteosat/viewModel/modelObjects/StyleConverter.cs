using System;
using meteosat.model;

namespace meteosat.viewModel.modelObjects
{
    class StyleConverter
    {
        public static StyleViewModel ModelToViewModel(Style model)
        {
            var name = Enum.GetName(typeof(Style), model);
            return (StyleViewModel)Enum.Parse(typeof(StyleViewModel), name);
        }

        public static Style ViewModelToModel(StyleViewModel viewModel)
        {
            var name = Enum.GetName(typeof(StyleViewModel), viewModel);
            return (Style)Enum.Parse(typeof(Style), name);
        }
    }
}
