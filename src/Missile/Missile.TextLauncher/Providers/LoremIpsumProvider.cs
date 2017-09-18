using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Providers
{
    [Export(typeof(IProvider))]
    public class LoremIpsumProvider : IProvider<string>
    {
        private const string Text =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut semper tortor, efficitur consectetur massa. Donec auctor sodales lacus vel dignissim. Suspendisse nec nulla sed orci commodo vehicula non ac est. Donec at aliquam lectus. Vivamus eros mi, malesuada non ex nec, faucibus maximus ligula. Sed ut nisl felis. Proin non urna et ligula mollis mattis nec a tortor.
Proin justo nunc, pretium ac pellentesque quis, tincidunt ut ligula. Morbi varius erat sapien, eget porta nibh sollicitudin auctor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Maecenas congue interdum libero, convallis vehicula mauris efficitur lobortis. Morbi viverra, est eget fermentum pulvinar, risus lorem posuere purus, eget commodo leo est sed lorem. Nunc erat velit, ultrices eget malesuada in, faucibus ac ex. Curabitur vulputate velit sed lacinia luctus. Duis non nisl at dolor rhoncus viverra. Donec id elit porttitor, auctor mi sit amet, imperdiet lectus. Integer ut ligula interdum, mattis nisl ut, sodales sapien.
Pellentesque lobortis tellus et justo aliquam consectetur. Vestibulum faucibus sem ut massa elementum, non pretium mauris ultricies. Etiam pharetra quam eu mauris sodales suscipit ac nec risus. Pellentesque quam mauris, pulvinar a nibh vitae, auctor fermentum ante. Pellentesque sit amet velit sit amet enim tempus placerat. Suspendisse nec mattis nisl. Duis ipsum libero, consectetur in risus eget, sodales aliquam arcu. In tempus nibh ac ligula luctus iaculis. In id magna ac nibh scelerisque fermentum. Aliquam lobortis enim non justo scelerisque cursus. Vivamus posuere turpis mollis tortor pellentesque, eu pharetra odio aliquet.
Donec commodo metus nisl, eu tempor turpis dignissim quis. Nam a enim eleifend, ultricies nisl eu, mattis sapien. Pellentesque erat magna, sollicitudin in nisl egestas, porta cursus magna. Aenean sit amet erat at ipsum varius placerat non at metus. Praesent convallis diam mauris, eget sagittis ipsum scelerisque id. Sed non viverra metus. Curabitur id tristique dui. Proin imperdiet, tellus id elementum egestas, lectus urna porta diam, eget finibus sem nisl eu turpis. Vestibulum nec nisl sit amet quam volutpat lacinia at at ex. Duis posuere fermentum nibh eu aliquet. Praesent at blandit nulla.
Morbi fringilla tellus ac quam porta, ultrices cursus nisl elementum. Aenean eu massa purus. Phasellus egestas pharetra neque, vel tincidunt felis interdum non. Integer posuere dictum eros, sed gravida ante consequat non. Integer sodales lorem arcu, in finibus justo congue eget. Nunc porta tortor sed ligula gravida, a convallis diam vestibulum. Sed at risus a nibh sagittis malesuada ut interdum metus. Praesent sit amet venenatis erat. Suspendisse dapibus ligula quis lacus condimentum, at maximus nulla pharetra. In sed ligula vitae sem consectetur auctor.
";

        public string Name { get; set; } = "lorem";

        public IObservable<string> Provide(string[] args)
        {
            return "Lorem ipsum dolor sit amet.".Split().ToObservable();
        }
    }
}