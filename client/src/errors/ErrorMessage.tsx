import errorGif from './error.gif'
import './errorMessage.scss'

const ErrorMessage = () => {
	return (
		<img src={errorGif} alt='character not found' className='errorMessage' />
	)
}

export default ErrorMessage