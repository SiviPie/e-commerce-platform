import '../../style/SubjectCompact.css'

const PostCompact = ({subject}) => {
    return(
        <div className = "subject-compact">
            <div className = "title">
                {subject.NumeSubiect}
            </div>
        </div>
    )
}

export default PostCompact;