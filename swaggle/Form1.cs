using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets; 

namespace swaggle
{
    public partial class Form1 : Form
    {

        private List<Socket> clients = new List<Socket>();
        private Thread listening_thread;
        private TcpListener listener;   

        public Form1()
        {
            InitializeComponent();
            this.listening_thread = new Thread(new ThreadStart(this.ListeningThread));
            this.listening_thread.Start(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


         private void ListeningThread() 
         // let's listen in another thread instead!!        
         {             
              
              int port = 443; 
              // change as required               
              this.listener = new TcpListener(IPAddress.Any, port);               
              try            
              {                
                  this.listener.Start();            
              }             
              catch (Exception e) 
              { 
                  MessageBox.Show("couldn't bind to port " + port + " -> " + e.Message); return; 
              }               
              while (true)             
              {                 
                  if (this.listener.Pending())                    
                      this.clients.Add(this.listener.AcceptSocket()); 
                  // won't block because pending was true                  
                  foreach (Socket sock in this.clients)                     
                      if (sock.Poll(0, SelectMode.SelectError))                         
                          clients.Remove(sock);                     
                      else if (sock.Poll(0, SelectMode.SelectRead))                         
                          ParserFunction(sock);                   
                  Thread.Sleep(30);             
              }         
          }
          private void ParserFunction(Socket sock) 
          { 

          } 
    }
}
