using System;
using UnityEngine;

namespace AchUtils.Ach
{
    /// <summary>
    /// 인스펙터에서 "상수 값"과 "Ach 변수 에셋" 중 하나를 선택해 쓸 수 있는 참조.
    /// 프로토타이핑 단계에선 상수로 빠르게, 공유가 필요해지면 변수 에셋으로
    /// 코드 수정 없이 전환할 수 있습니다.
    /// </summary>
    /// <typeparam name="T">값의 타입.</typeparam>
    /// <typeparam name="TVariable">대응하는 Ach 변수 타입.</typeparam>
    [Serializable]
    public abstract class AchReference<T, TVariable> where TVariable : AchVariable<T>
    {
        [SerializeField]
        [Tooltip("켜면 상수 값을, 끄면 변수 에셋을 사용합니다.")]
        private bool useConstant = true;

        [SerializeField]
        private T constantValue;

        [SerializeField]
        private TVariable variable;

        protected AchReference()
        {
        }

        protected AchReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }

        protected AchReference(TVariable variable)
        {
            useConstant = false;
            this.variable = variable;
        }

        /// <summary>상수 값을 사용하는지 여부.</summary>
        public bool UseConstant
        {
            get => useConstant;
            set => useConstant = value;
        }

        /// <summary>상수 모드에서 사용하는 값.</summary>
        public T ConstantValue
        {
            get => constantValue;
            set => constantValue = value;
        }

        /// <summary>변수 모드에서 사용하는 Ach 변수 에셋.</summary>
        public TVariable Variable
        {
            get => variable;
            set => variable = value;
        }

        /// <summary>
        /// 현재 모드에 따라 결정된 값. 변수 모드인데 변수가 비어 있으면 기본값을 돌려줍니다.
        /// </summary>
        public T Value
        {
            get
            {
                if (useConstant || variable == null)
                {
                    return useConstant ? constantValue : default;
                }

                return variable.Value;
            }
            set
            {
                if (useConstant || variable == null)
                {
                    constantValue = value;
                }
                else
                {
                    variable.Value = value;
                }
            }
        }

        public static implicit operator T(AchReference<T, TVariable> reference)
        {
            return reference.Value;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? "null";
        }
    }
}
