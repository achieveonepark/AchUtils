using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Formula
{
    public class FormulaEvaluator
    {
        private string _input;
        private int _pos;
        private FormulaContext _context;

        public float Evaluate(string expression, FormulaContext context)
        {
            _input = expression.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            _pos = 0;
            _context = context;
            float result = ParseExpression();
            if (_pos != _input.Length)
                throw new Exception($"Unexpected character '{_input[_pos]}' at position {_pos}");
            return result;
        }

        private float ParseExpression()
        {
            float left = ParseTerm();
            while (_pos < _input.Length && (_input[_pos] == '+' || _input[_pos] == '-'))
            {
                char op = _input[_pos++];
                float right = ParseTerm();
                left = op == '+' ? left + right : left - right;
            }
            return left;
        }

        private float ParseTerm()
        {
            float left = ParseFactor();
            while (_pos < _input.Length && (_input[_pos] == '*' || _input[_pos] == '/'))
            {
                char op = _input[_pos++];
                float right = ParseFactor();
                left = op == '*' ? left * right : left / right;
            }
            return left;
        }

        private float ParseFactor()
        {
            if (_pos < _input.Length && _input[_pos] == '-')
            {
                _pos++;
                return -ParseFactor();
            }

            if (_pos < _input.Length && _input[_pos] == '(')
            {
                _pos++;
                float value = ParseExpression();
                Expect(')');
                return value;
            }

            if (_pos < _input.Length && (char.IsDigit(_input[_pos]) || _input[_pos] == '.'))
                return ParseNumber();

            if (_pos < _input.Length && (char.IsLetter(_input[_pos]) || _input[_pos] == '_'))
                return ParseIdentifierOrFunction();

            throw new Exception($"Unexpected character at position {_pos}");
        }

        private float ParseNumber()
        {
            int start = _pos;
            while (_pos < _input.Length && (char.IsDigit(_input[_pos]) || _input[_pos] == '.'))
                _pos++;
            return float.Parse(_input.Substring(start, _pos - start));
        }

        private float ParseIdentifierOrFunction()
        {
            int start = _pos;
            while (_pos < _input.Length && (char.IsLetterOrDigit(_input[_pos]) || _input[_pos] == '_'))
                _pos++;
            string name = _input.Substring(start, _pos - start);

            if (_pos < _input.Length && _input[_pos] == '(')
            {
                _pos++;
                var args = new List<float>();
                while (_pos < _input.Length && _input[_pos] != ')')
                {
                    args.Add(ParseExpression());
                    if (_pos < _input.Length && _input[_pos] == ',') _pos++;
                }
                Expect(')');
                return CallFunction(name, args);
            }

            return _context.Get(name);
        }

        private float CallFunction(string name, List<float> args)
        {
            return name switch
            {
                "Random" => UnityEngine.Random.Range(args[0], args[1]),
                "Min" => Mathf.Min(args[0], args[1]),
                "Max" => Mathf.Max(args[0], args[1]),
                "Abs" => Mathf.Abs(args[0]),
                "Clamp" => Mathf.Clamp(args[0], args[1], args[2]),
                "Floor" => Mathf.Floor(args[0]),
                "Ceil" => Mathf.Ceil(args[0]),
                "Round" => Mathf.Round(args[0]),
                "Sqrt" => Mathf.Sqrt(args[0]),
                "Pow" => Mathf.Pow(args[0], args[1]),
                "Sign" => Mathf.Sign(args[0]),
                _ => throw new Exception($"Unknown function: {name}")
            };
        }

        private void Expect(char c)
        {
            if (_pos >= _input.Length || _input[_pos] != c)
                throw new Exception($"Expected '{c}' at position {_pos}");
            _pos++;
        }
    }
}
