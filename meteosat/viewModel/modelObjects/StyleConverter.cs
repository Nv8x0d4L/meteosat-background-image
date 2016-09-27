using System;
using meteosat.model;

namespace meteosat.viewModel.modelObjects
{
    class StyleConverter
    {
        private const string ModelCouldNotBeConverted = "model {0} could not be converted.";
        private const string UnableToParseStyleviewmodelFromValue = "Unable to parse StyleViewModel from value {0}";
        private const string UnableToParseStyleFromValue = "Unable to parse Style from value {0}";
        private const string ViewmodelCouldNotBeConverted = "viewModel {0} could not be converted.";

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
