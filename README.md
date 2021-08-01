# TCP_Server_Client_System
- This system is used to send message from a client to the other clients.
- When a client connected or disconnected, the situation can be seen on the server console screen.
- In addition, the number of connected clients is shown in every connection or disconnection process on the server console screen.
- To show how to manipulate data, I set a sample condition in source of client (line 56). If we send "Update" message from a client the others will print "Updating" message.
# Source
- I used the following code as a referance. However, the code had some bugs. For example, when a client is terminated, the server was throwing some exceptions, and this was causing the down of the server. I fixed the problem. Now, when we close a client, it just prints the message about a client is disconnected; and the TcpClient object is removed on the tcpClientList collection.
- http://snippetbank.blogspot.com/2014/04/csharp-client-server-broadcast-example-1.html
