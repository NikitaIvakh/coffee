import React from 'react'
import ErrorMessage from 'errors/ErrorMessage'
import Spinner from 'components/Spinner/Spinner'

type DataProps = {
	data: unknown
}

type ComponentTypeWithProps = React.ComponentType<DataProps>;

const SetContentList = (Component: ComponentTypeWithProps, process: string, data: unknown) => {
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
