using System;
using System.Runtime.ExceptionServices;
using Cysharp.Threading.Tasks;

namespace UniMob.Utils
{
    public static class AsyncAtom
    {
        public static AsyncAtom<T> FromUniTask<T>(Lifetime lifetime, Func<UniTask<T>> func)
        {
            return new AsyncAtom<T>(lifetime, func);
        }
    }

    public class AsyncAtom<T>
    {
        private readonly Atom<AsyncValue<T>> _atom;
        private readonly Func<UniTask<T>> _func;

        private bool _initialized;
        private AsyncValue<T> _value;
        private ExceptionDispatchInfo _exception;

        internal AsyncAtom(Lifetime lifetime, Func<UniTask<T>> func)
        {
            _func = func;
            _atom = Atom.Computed(lifetime, Compute);
        }

        public AsyncValue<T> Value
        {
            get
            {
                if (!_initialized)
                {
                    _initialized = true;
                    Refresh();
                }

                return _atom.Value;
            }
        }

        public void Refresh()
        {
            using var _ = Atom.NoWatch;

            _value = new AsyncValue<T>(true, default);
            _exception = null;
            _atom.Invalidate();
            Load(_func.Invoke()).Forget();
        }

        private AsyncValue<T> Compute()
        {
            _exception?.Throw();
            return _value;
        }

        private async UniTaskVoid Load(UniTask<T> task)
        {
            try
            {
                _value = new AsyncValue<T>(false, await task);
                _exception = null;
            }
            catch (Exception ex)
            {
                _exception = ExceptionDispatchInfo.Capture(ex);
            }

            _atom.Invalidate();
        }
    }

    public readonly struct AsyncValue<T>
    {
        public bool Loading { get; }
        public T Value { get; }

        public AsyncValue(bool loading, T value)
        {
            Loading = loading;
            Value = value;
        }

        public bool TryGetValue(out T value)
        {
            value = Value;
            return Loading == false;
        }
    }
}