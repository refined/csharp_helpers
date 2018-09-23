using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

namespace CsharpHelpers.Standard
{
    public static class ReactiveExtensions
    {
        public static IObservable<T> TakeUntilCancelled<T>(this IObservable<T> ob, CancellationToken ct)
        {
            return ob.TakeUntil(Observable.Create<Unit>(o => ct.Register(() =>
            {
                o.OnNext(Unit.Default);
                o.OnCompleted();
            })));
        }
    }

}
