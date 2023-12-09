namespace Day_8
{
    internal class Node
    {
        public Node(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; set; }
        public Node? LeftNode { get; set; }
        public Node? RightNode { get; set; }
    }
}
