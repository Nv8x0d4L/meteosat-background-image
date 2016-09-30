using System;
using meteosat.model;
using meteosat.util;

namespace meteosat.viewModel.modelObjects
{
    class StyleConverter
    {
        private static readonly string ModelCouldNotBeConverted = DefaultValues.GetString("ModelCouldNotBeConverted");
        private static readonly string UnableToParseStyleviewmodelFromValue = DefaultValues.GetString("UnableToParseStyleviewmodelFromValue");
        private static readonly string UnableToParseStyleFromValue = DefaultValues.GetString("UnableToParseStyleFromValue");
        private static readonly string ViewmodelCouldNotBeConverted = DefaultValues.GetString("ViewmodelCouldNotBeConverted");

        public static StyleViewModel ModelToViewModel(Style model)
        {
            var name = Enum.GetName(typeof(Style), model);
            if (name != null)
            {
                return ViewModelFromString(name);
            }
            else
            {
                throw new Exception(string.Format(ModelCouldNotBeConverted, model));
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
                throw new Exception(string.Format(UnableToParseStyleviewmodelFromValue, name));
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
                throw new Exception(string.Format(ViewmodelCouldNotBeConverted, viewModel));
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
                throw new Exception(string.Format(UnableToParseStyleFromValue, name));
            }
        }
    }
}
