using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChatGptHelper.Models
{
    internal class PropertyRepository
    {
        List<PropertyState> _properties;

        public PropertyRepository()
        {
            _properties = new List<PropertyState>()
            {
                new PropertyState(ChatState.Question, "Вопрос", Brushes.Black,false),
                new PropertyState(ChatState.Answer, "Ответ", Brushes.DarkMagenta,true),
                new PropertyState(ChatState.Answer, "Ожидание", Brushes.DarkMagenta,true)
            };
        }
        public PropertyRepository(params PropertyState[] propertyStates) => _properties = propertyStates.ToList();
        public PropertyState GetPropertyState(ChatState chatState) => _properties.FirstOrDefault(i =>  i == chatState) ?? null;        
    }
}
