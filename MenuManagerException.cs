using System;

public class MenuManagerException : Exception{
    public MenuManagerException(string message) : base(message) { }
}

public class MenuScriptNotFoundException : MenuManagerException{
    public MenuScriptNotFoundException(string message) : base(message) { }
}

public class InvalidMenuScriptNameException : MenuManagerException{
    public InvalidMenuScriptNameException(string message) : base(message) { }
}

public class MenuScriptMethodNotFoundException : MenuManagerException{
    public MenuScriptMethodNotFoundException(string message) : base(message) { }
}

public class InvalidAmountParametersException : MenuManagerException
{
    public InvalidAmountParametersException(string message) : base(message) { }
}