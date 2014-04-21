namespace Cop.Theia.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    using Cop.Theia.Contract;

    using FirstFloor.ModernUI.Presentation;

    [ValueConversion(typeof(IEnumerable<IModule>), typeof(LinkGroupCollection))]
    internal class ModulesToLinkGroupsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modules = value as IEnumerable<IModule>;

            if (modules == null)
            {
                throw new ArgumentException();
            }

            var linkGroups = new LinkGroupCollection();

            var aggregatedTopics = modules
                .SelectMany(module => module.Topics)
                .GroupBy(topic => topic.Name)
                .SelectMany(group => group);

            foreach (var aggregatedTopic in aggregatedTopics)
            {
                var linkGroup = new LinkGroup() { DisplayName = aggregatedTopic.Name };

                aggregatedTopic
                    .Subtopics
                    .Select(subtopic => new Link() { DisplayName = subtopic.Name, Source = subtopic.SourceUri })
                    .ToList()
                    .ForEach(linkGroup.Links.Add);

                linkGroups.Add(linkGroup);
            }

            return linkGroups;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}