using System;
using System.Messaging;

namespace HPF.FutureState.Common.Utils
{
    public class HPFSummaryQueue
    {
        /// <summary>
        /// Put a completed Case to MSMQ
        /// </summary>
        /// <param name="fc_id"></param>
        public void SendACompletedCaseToQueue(string fc_id)
        {
            var entry = new HPFSummaryQueueEntry {FC_ID = fc_id};
            SendACompletedCaseToQueue(entry);            
        }

        /// <summary>
        /// Put a completed Case Entry to MSMQ
        /// </summary>
        /// <param name="entry"></param>
        public void SendACompletedCaseToQueue(HPFSummaryQueueEntry entry)
        {            
            var queue = GetMessageQueue();
            var message = CreateQueueMessage(entry);
            using (var queueTransaction = new MessageQueueTransaction())
            {                
                try
                {
                    queueTransaction.Begin();
                    queue.Send(message, queueTransaction);                    
                    queueTransaction.Commit();
                }
                finally
                {
                    queue.Close();
                }
            }
        }

        /// <summary>
        /// ReceiveCompletedCaseFromQueue, null: if there are no CompletedCase in the Queue.
        /// </summary>
        /// <returns>HPFSummaryQueueEntry</returns>
        public HPFSummaryQueueEntry ReceiveCompletedCaseFromQueue()
        {
            var queue = GetMessageQueue();
            try
            {
                var message = queue.Receive(new TimeSpan(0, 0, 0, Constant.HPF_QUEUE_READING_TIMEOUT));
                queue.Close();
                if (message != null)
                {
                    message.Formatter = new BinaryMessageFormatter();
                    return (HPFSummaryQueueEntry)message.Body;
                }
            }
            catch (MessageQueueException)
            {
                return null;
            }
            finally
            {
                queue.Close();
            }
            return null;
        }

        private static Message CreateQueueMessage(HPFSummaryQueueEntry entry)
        {
            return new Message(entry,new BinaryMessageFormatter()) { Label = ("FC_ID : " + entry.FC_ID) };
        }

        private static MessageQueue GetMessageQueue()
        {
            MessageQueue mq;
            if (MessageQueue.Exists(Constant.HPF_QUEUE_PATH))
            {
                mq = new MessageQueue(Constant.HPF_QUEUE_PATH, true);
            }
            else
                mq = MessageQueue.Create(Constant.HPF_QUEUE_PATH, true);
            return mq;
        }        
    }
}
