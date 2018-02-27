from zmq_tools import *


ctx = zmq.Context()
requester = ctx.socket(zmq.REQ)
requester.connect('tcp://localhost:50020')

requester.send_string('SUB_PORT')
ipc_sub_port = requester.recv_string()
monitor = Msg_Receiver(ctx,'tcp://localhost:%s'%ipc_sub_port,topics=('notify.',))

while True:
    print(monitor.recv())


