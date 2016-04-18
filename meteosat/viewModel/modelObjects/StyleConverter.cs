using System;
using meteosat.model;

namespace meteosat.viewModel.modelObjects
{
    class StyleConverter
    {
        public static StyleViewModel ModelToViewModel(Style model)
        {
            var name = Enum.GetName(typeof(Style), model);
            if (name != null)
            {
                return ViewModelFromString(name);
            }
            else
            {
                throw new Exception($"model {model} could not be converted.");
            }
        }

        public static StyleViewModel ViewModelFromString(string name)
        {
            StyleViewModel viewModel;
            if (Enum.TryParse(name, true, out viewModel))
            {
                return viewModel;
            }
            else
            {
                throw new Exception($"Unable to parse StyleViewModel from value {name}");
            }
        }


        public static Style ViewModelToModel(StyleViewModel viewModel)
        {
            var name = Enum.GetName(typeof(StyleViewModel), viewModel);
            if (name != null)
            {
                return ModelFromString(name);
            }
            else
            {
                throw new Exception($"viewModel {viewModel} could not be converted.");
            }
        }

        public static Style ModelFromString(string name)
        {
            Style model;
            if (Enum.TryParse(name, true, out model))
            {
                return model;
            }
            else
            {
                throw new Exception($"Unable to parse Style from value {name}");
            }
        }
    }
}
