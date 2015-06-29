using System;
	
public class Message {

	public IEnums.MessageType type;
	public string text;

	public Message (string text, IEnums.MessageType type) {
		this.type = type;
		this.text = text;
	}
}

