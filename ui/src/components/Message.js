import '../style/Message.css'

const Message = ({message}) => {
    return(
        <div className="message">
            <p>{message}</p>
        </div>
    )
}

export default Message;