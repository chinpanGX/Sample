#nullable enable
using System;
using System.Collections.Generic;

namespace App.Common.StateMachine
{
    public class StateMachine<T> where T : class
    {
        public interface IState : IDisposable
        {
            void Clear();
            void Begin(T owner);
            void Execute(T owner);
            void End(T owner);
        }

        public class State : IState
        {
            public virtual void Dispose()
            {

            }
            public virtual void Clear()
            {

            }
            public virtual void Begin(T owner)
            {

            }
            public virtual void Execute(T owner)
            {

            }
            public virtual void End(T owner)
            {

            }
        }

        T owner = default!;
        IState current = null!;
        IState request = null!;
        List<IState> cache = new List<IState>();
        IState Current => current;

        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// 後始末
        /// </summary>
        public void Dispose()
        {
            if (current != null)
            {
                current.End(owner);
                Free(current);
            }
            current = null!;

            foreach (var state in cache)
            {
                state.Dispose();
            }
            cache.Clear();
        }

        /// <summary>
        /// ステートを切り替える
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        public void Change<TState>() where TState : IState, new()
        {
            request = Alloc<TState>();
        }

        /// <summary>
        /// 実行
        public void Execute()
        {
            if (request != null)
            {
                if (current != null)
                {
                    current.End(owner);
                    Free(current);
                }

                current = request;
                request = null!;

                current.Begin(owner);
            }

            Current.Execute(owner);
        }

        /// <summary>
        /// 現在のステートを確認
        /// </summary>
        public bool IsState<TState>() where TState : IState
        {
            return current is TState;
        }

        /// <summary>
        /// ステートの確保
        /// </summary>
        IState Alloc<TState>() where TState : IState, new()
        {
            foreach (var value in cache)
            {
                if (value is TState state)
                {
                    return state;
                }
            }
            return new TState();
        }

        /// <summary>
        /// ステートの解放
        /// </summary>
        void Free<TState>(TState state) where TState : IState
        {
            state.Clear();
            cache.Add(state);
        }
    }
}