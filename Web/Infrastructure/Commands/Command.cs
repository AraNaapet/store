using System;
using System.Linq.Expressions;

namespace Marina.Store.Web.Infrastructure.Commands
{
    /// <summary>
    /// ������� ����� ��� �������
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// ��� ������� ����� ��������� ��������� ����������
        /// </summary>
        public State Success;

        /// <summary>
        /// ������� ���������, ��������������� ���������� � ��������� � �������
        /// </summary>
        protected Outcome Fail(Expression<Func<State>> stateProperty, string description)
        {
            var name = ExtractProperty(stateProperty);
            if (name == ExtractProperty(()=>Success))
            {
                throw new InvalidOperationException("����� Fail �� ����� ������� ��������� Success");
            }
            return new ErrorOutcome(name, description);
        }

        /// <summary>
        /// ������� ���������, ��������������� ��������� ���������� ��������
        /// </summary>
        protected Outcome Ok(string description = null)
        {
            return new SuccessOutcome(description);
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        protected Result<T> Value<T>(T value, string description = null)
        {
            return new Result<T>(new SuccessOutcome(description), value);
        }

        private static string ExtractProperty<T>(Expression<Func<T>> property)
        {
            var expression = (MemberExpression)property.Body;
            return expression.Member.Name;
        }
    }
}