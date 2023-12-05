# MathParser

        public static double Function(double x, char[] Expression) 
        {
            Parser parser = new Parser(Expression);
            parser.ParseSemicolon(x);
            return parser.Root;
        }

      Метод для работы с веденным уравнением / A method for working with a stored expression

      P.S. Это часть курсовой работы на которую я так и не смог найти ответ. Не самый оптимизированный вариант решения своей задачи. 
      Класс как конструктор –  можно добавить метод который обрабатывает массив с выражением из TextBox. 
      И для уточнений, я не программист, программа только для курсовой работы и возможно не будет обновляться
      (При этом никто не мешает вам написать предложение по обновлению кода для других пользователей)/
      This is a part of the course work that I have not been able to find an answer to. 
      This is not the most optimized way to solve your problem. 
      Class as a constructor – you can add a method that processes an array with an expression from a TextBox.
      And to clarify, I am not a programmer, the program is only for coursework and may not be updated 
      (While no one prevents you from writing a proposal to update the code for other users)
