import ErrorMessage from '../errors/ErrorMessage'
import Spinner from '../Spinner/Spinner'

const SetContentList = (Component, process, data) => {
	switch (process) {
		case 'waiting':
			return <Spinner />
		case 'loading':
			return <Spinner />
		case 'confirmed':
			return <Component />
		case 'error':
			return <ErrorMessage />
		default:
			throw new Error('Unexpected process state')
	}
}

export default SetContentList