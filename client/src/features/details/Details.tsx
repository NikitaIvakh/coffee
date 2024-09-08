import './details.scss'
import { useMemo } from 'react'
import { useParams } from 'react-router-dom'
import { type CoffeeById } from 'types'
import SetContentList from 'utils/SetContentList'
import Info from './Info'
import useCoffee from './use-coffee'

interface DetailsProps {
	path: string
}

type RouteParams = {
	id: string
}

const Details = (props: DetailsProps) => {
	const { path } = props
	const { id } = useParams<RouteParams>()
	const { coffee, status } = useCoffee(id!)
	
	const renderItems = (coffee: CoffeeById) => {
		return (
			<Info path={path} {...coffee} />
		)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(coffee!), status, coffee)
	}, [status, coffee, renderItems])
	
	return (
		<div className='details'>
			<div className='details__wrapper'>
				{elements}
			</div>
		</div>
	)
}

export default Details