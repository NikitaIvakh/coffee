import './header.scss'

const Header = () => {
	return (
		<header className='header'>
			<div className='header__wrapper'>
				<ul className='header__list'>
					<li className='header__item'>Coffee house</li>
					<li className='header__item'>Our coffee</li>
					<li className='header__item'>For your pleasure</li>
				</ul>
			</div>
		</header>
	)
}

export default Header
