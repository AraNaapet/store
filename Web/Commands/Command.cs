using System.Collections.Generic;

namespace Marina.Store.Web.Commands
{
    /// <summary>
    /// ������� ����� ��� �������
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// ������� ������
        /// </summary>
        protected CommandResult<T> Result<T>(T data)
        {
            return new CommandResult<T>(data);
        }

        /// <summary>
        /// ������� ����������� �� �������� ���������� ��������
        /// </summary>
        protected CommandResult Success()
        {
            return new CommandResult();
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        protected CommandResult<T> Fail<T>(string key, string value)
        {
            return new CommandResult<T>(new Dictionary<string, string> { { key, value } });
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        protected CommandResult Fail(string key, string value)
        {
            return new CommandResult(new Dictionary<string, string> {{key, value}});
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        protected CommandResult<T> Fail<T>(IDictionary<string, string> errors)
        {
            return new CommandResult<T>(errors);
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        protected CommandResult Fail(IDictionary<string, string> errors)
        {
            return new CommandResult(errors);
        }
    }
}