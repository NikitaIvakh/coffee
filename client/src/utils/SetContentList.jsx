import ErrorMessage from '../errors/ErrorMessage'
import Spinner from '../components/Spinner/Spinner'

const SetContentList = (Component, process, data) => {
	switch (process) {
		case 'waiting':
		case 'loading':
			return <Spinner />
		case 'confirmed':
			return <Component data={data} />
		case 'error':
			return <ErrorMessage />
		default:
			return null
	}
}

export default SetContentList
