import './details.scss'
import { useMemo } from 'react'
import { useParams } from 'react-router-dom'
import SetContentList from '../../utils/SetContentList'
import Info from './Info'
import useCoffee from './use-coffee'

const Details = () => {
	const { id } = useParams()
	const { coffee, status } = useCoffee(id)
	
	const renderItems = (coffee) => {
		return (
			<Info {...coffee} />
		)
	}
	
	const elements = useMemo(() => {
		return SetContentList(() => renderItems(coffee), status, coffee)
	}, [id, status, coffee])
	
	return (
		<div className='details'>
			<div className='details__wrapper'>
				{elements}
			</div>
		</div>
	)
}

export default Details