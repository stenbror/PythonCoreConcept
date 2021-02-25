using System;

namespace PythonCoreConcept.Parser
{
    public record Token(UInt32 StartPos, UInt32 EndPos, TokenKind Kind, Trivia[] PrefixTrivia);

    public record NameToken(UInt32 StartPos, UInt32 EndPos, Trivia[] PrefixTrivia, string Text) 
        : Token(StartPos, EndPos, TokenKind.Name, PrefixTrivia);
    
    public record NumberToken(UInt32 StartPos, UInt32 EndPos, Trivia[] PrefixTrivia, string Text) 
        : Token(StartPos, EndPos, TokenKind.Number, PrefixTrivia);
    
    public record StringToken(UInt32 StartPos, UInt32 EndPos, Trivia[] PrefixTrivia, string Text) 
        : Token(StartPos, EndPos, TokenKind.String, PrefixTrivia);
    
    public record TypeCommentToken(UInt32 StartPos, UInt32 EndPos, Trivia[] PrefixTrivia, string Text) 
        : Token(StartPos, EndPos, TokenKind.TypeComment, PrefixTrivia);
}