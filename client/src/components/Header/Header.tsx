import './header.scss'
import { NavLink } from 'react-router-dom'

const Header = () => {
	return (
		<header className='header'>
			<div className='header__wrapper'>
				<ul className='header__list'>
					<NavLink to='/' className='header__item'>Coffee house</NavLink>
					<NavLink to='/OurCoffee' className='header__item'>Our coffee</NavLink>
					<NavLink to='/Pleasure' className='header__item'>For your pleasure</NavLink>
				</ul>
			</div>
		</header>
	)
}

export default Header
